The goal of this repo is to deploy a dotnet web server that can handle open mic sign ups via text by interacting with the twilio api while deployed on heroku as a container via docker
# road map:
- [x] deploy to heroku
- [x] verify endpoint is live
- [x] establish basic classes for an open mic
- [ ] establish connection to the twilio api
- [ ] modify the target for twilio's api by updating your endpoint
- [ ] receive text from your personal number and print body of message to the console
- [ ] upon receipt of message return a preprogrammed message to sender
- [ ] upon receipt of message, validate
- [ ] upon receipt of message, determine if:
- [ ] sender is a host
- [ ] if so, parse and validate command
- [ ] if not, does roomcode exist? tell user either way. 
