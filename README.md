# VEAssessment - OneTimePassword

Implementation of an OTP algorithm to generate a password with a maximum life span of 30 seconds. If, for example, the password is generated after the first 10s of a minute (06:05:10), it will be valid for 20s only, until (06:05:29) right before the counter is increased. In order to ensure it would last 30s, on this implementation we would have to accept passwords from the current and previous window of creation, this passwords could last longer than 30s.
