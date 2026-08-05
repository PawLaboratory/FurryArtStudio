import calendar
import datetime
import os
import pyotp
import subprocess
import sys
import time

def timecode(for_time: datetime.datetime, interval: int) -> int:
    if for_time.tzinfo:
        return int(calendar.timegm(for_time.utctimetuple())) % interval
    else:
        return int(time.mktime(for_time.timetuple())) % interval

def normalize_sha1(sha1: str) -> str:
    return ''.join(c for c in sha1 if c.isalnum())

def sign_file(fingerprint: str, file_path: str, signtool_exe: str, max_retries: int = 3) -> None:
    """Sign a single file with retry logic."""
    for attempt in range(max_retries):
        result = subprocess.run(
            [
                signtool_exe, 'sign',
                '/sha1', fingerprint,
                '/tr', 'http://time.certum.pl',
                '/td', 'sha256',
                '/fd', 'sha256',
                '/v', file_path
            ],
            capture_output=True,
            text=True,
            encoding='utf-8'
        )
        if result.returncode == 0 and "Error" not in result.stdout and "Error" not in result.stderr:
            print(f"[SUCCESS] Signed: {os.path.basename(file_path)}")
            return
        if attempt < max_retries - 1:
            time.sleep(2)
    raise RuntimeError(f"Failed to sign {file_path} after {max_retries} attempts.")

if __name__ == "__main__":
    print("=== Sign script started ===", flush=True)
    username = os.getenv('SIGN_USERNAME')
    otp_token = os.getenv('SIGN_OTP_TOKEN')
    if not username or not otp_token:
        raise RuntimeError("SIGN_USERNAME or SIGN_OTP_TOKEN missing.")

    if len(sys.argv) < 2:
        raise RuntimeError("Usage: python sign.py <publish_path>")
    publish_path = sys.argv[1]
    if not os.path.isdir(publish_path):
        raise RuntimeError(f"Publish path not found: {publish_path}")

    signtool_exe = os.path.join(os.path.dirname(os.path.abspath(__file__)), "signtool.exe")
    if not os.path.exists(signtool_exe):
        raise RuntimeError("signtool.exe not found")

    totp = pyotp.TOTP(otp_token, digest='SHA256', issuer='Certum')

    # Wait for OTP window
    print("\nAuthenticating with SimplySignDesktop...")

    current_otp = totp.now()
    print(f"OTP: {current_otp}")

    proc = subprocess.Popen(
        [r'C:\Program Files\Certum\SimplySign Desktop\SimplySignDesktop.exe',
         '/autologin', username, str(current_otp)],
        stdout=subprocess.PIPE,
        stderr=subprocess.STDOUT,
        text=True,
        encoding='utf-8',
        creationflags=0x08000000  # CREATE_NO_WINDOW
    )

    fingerprint = None
    try:
        # Read first line for fingerprint
        print("Waiting for SimplySign fingerprint...", flush=True)
        start_time = time.time()

        while True:
            if time.time() - start_time > 60:
                raise RuntimeError("Timeout waiting for SimplySign fingerprint.")

            line = proc.stdout.readline()

            if line:
                print("SimplySign output:", repr(line), flush=True)
                fingerprint = normalize_sha1(line.strip())
                break

            if proc.poll() is not None:
                raise RuntimeError("SimplySign exited unexpectedly.")

            time.sleep(0.1)

        if not fingerprint:
            raise RuntimeError(
                "No fingerprint received. Authentication may have failed."
            )

        print(f"Fingerprint: {fingerprint}", flush=True)

        # Sign only specified files
        files_to_sign = [
            os.path.join(publish_path, "FurryArtStudio.exe"),
            os.path.join(publish_path, "Chromis.dll")
        ]
        for f in files_to_sign:
            if os.path.exists(f):
                sign_file(fingerprint, f, signtool_exe)
            else:
                print(f"[WARNING] File not found: {f}")

        print("\n=== Signing completed ===")
    finally:
        proc.terminate()