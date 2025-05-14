# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)

# NGROK
START CMD: ngrok http --region=us --domain=uniek.ngrok.io https://localhost:7260

# ShipStation
API Documentation: https://www.shipstation.com/docs/api/
Set Up Webhook: https://ship9.shipstation.com/settings/integrations/Webhooks
Webhook URL Dev: https://uniek.ngrok.io/api/shipstation/order-shipped?api-key=pDK7jTNdo9Qm4VhF5rEjg0sL6vS5CwQnYXZ9pM3r 
Webhook URL Prod: https://entegro-connect-fqa8l.ondigitalocean.app/api/shipstation/order-shipped?api-key=pDK7jTNdo9Qm4VhF5rEjg0sL6vS5CwQnYXZ9pM3r

# Build And Push Docker Image to Digital Ocean
docker build . -t entegro-connect:v1
docker tag entegro-connect:v1 registry.digitalocean.com/uniek-software/entegro-connect:v1
docker push registry.digitalocean.com/uniek-software/entegro-connect:v1

