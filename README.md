The goal of this repo is to deploy a dotnet web server that can handle open mic sign ups via text by interacting with the twilio api while deployed on heroku as a container via docker
# road map:
- [x] deploy to heroku
- [x] verify endpoint is live
- [x] establish basic classes for an open mic
- [x] establish connection to the twilio api
- [x] modify the target for twilio's api by updating your endpoint
- [x] send message to your phone
- [x] receive text from your personal number and print body of message to the console
- [x] upon receipt of message return a preprogrammed message to sender
- [x] upon receipt of message, validate
- [x] upon receipt of message, determine if:
- [x] sender is a host
- [x] if so, parse and validate command
- [ ] if not, does roomcode exist? tell user either way. 
