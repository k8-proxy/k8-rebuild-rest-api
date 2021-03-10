# K8 Rebuild REST API
## Setup
![k8-rebuild-api-workflow](imgs/k8_rebuild_rest_api_sequence_diagram.png)
- clone the repo    
    `git clone https://github.com/k8-proxy/k8-rebuild-rest-api.git`
- update modules    
    `git submodule update --init`

## Endpoints

| API Endpoint | Method | Description | 
|------|---------|---------    |
| /api/rebuild    | POST |  Rebuilds a file located at a specified URL and outputs the rebuilt file to another specified URL |
| /api/rebuild/file    | POST |  Rebuilds a file using its binary data       |
| /api/rebuild/base64   | POST | Rebuilds a file using the Base64 encoded representation |
| /api/rebuild/zipfile    | POST | Rebuilds a zip file using its binary data   |
| /api/rebuild/s3tozip  | POST | Rebuilds a zip file using S3 presignedURL/objectURL and This api requires s3 configuration(AWS key and secret) on the hosted server.      |
| /api/rebuild/s3tos3   | POST | Rebuilds a file using the Base64 encoded representation |
| /api/rebuild/zipfile    | POST | Rebuilds a zip file using S3 presignedURL/objectURL and upload the rebuilt zip file to targetPresignedURL/objectURL. This api requires s3 configuration(AWS key and secret) on the hosted server.  |
| /api/rebuild/ziptos3  | POST | Rebuilds a zip file using its binary data and This api requires s3 configuration(AWS key and secret) on the hosted server. |


## Detailed API Endpoints Documentation - [ Link ](./ApiEndpointsDocumentation.md)

## Environment Variables
These are static configuration that is used by the app to connect with other components.

- `AWS_ACCESS_KEY_ID` : access key for AWS Account being used for s3.
- `AWS_SECRET_ACCESS_KEY` : secret key for AWS Account being used for s3.

## Customize watermark on pdfs
 
 - A watermark is added only to pdfs once it is rebuilt by icap-server
 
 - Default watermark is `Glasswall Protected`
 
 - To customize watermark text, pass below parameter in the request body under `"PdfContentManagement"`
    ```
     "Watermark": "GW Certified"
    ```    
**Example:**
```
"PdfContentManagement": {
    "Watermark": "GW Certified"
  },
```

## Deployment
There are 2 ways to deploy K8 Rebuild Rest API; docker and runtime. Below are instructions for both docker and runtime deployments.

### Docker Deployment
- Change your directory     
    `cd k8-rebuild-rest-api`
- Update the `libs` submodule to the latest commit from `master` branch. Everytime the evaluation license expires, the submodule should be updated.
    `git submodule foreach git pull origin master`
- Build and run the docker image
    ```
    docker build -t sow-rest-api --file Source/Service/Dockerfile .
    docker run -it --rm -p 80:80 sow-rest-api
    ```
### Runtime Deployment
- Install the ASP.NET Core Runtime
    
    - On **Ubuntu 20.04 (LTS)**
        ```
        wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
        sudo dpkg -i packages-microsoft-prod.deb
        ```
    - Install the SDK
        ```
        sudo apt-get update; \
        sudo apt-get install -y apt-transport-https && \
        sudo apt-get update && \
        sudo apt-get install -y dotnet-sdk-5.0
        ```
    - Install the runtime
        ```
        sudo apt-get update; \
        sudo apt-get install -y apt-transport-https && \
        sudo apt-get update && \
        sudo apt-get install -y aspnetcore-runtime-3.1
        ```
    - Please check [supported distributions](https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu#install-the-runtime) of .NET on ubuntu.
- Change directory  
    `cd k8-rebuild-rest-api.git/Source/Service`
- Run   
    `dotnet run`

## Helm Chart
You can see the helm chart [here](https://github.com/k8-proxy/k8-rebuild-rest-api/blob/main/chart/README.md).

## Use Cases
- Process images that are retrieved from un-trusted sources
- Ability to use zip files in S3 buckets to provide the files needed to be rebuild
- Detect when files get dropped > get the file > unzip it > put all the files thought the Glasswall engine > capture all rebuilt files in one folder > capture all xml files in another folder > zip both folders > upload zip files to another S3 location

# Video Demo

https://www.youtube.com/watch?v=TlXwsJrXe68&amp;feature=youtu.be

# Postman Collection
https://www.getpostman.com/collections/8eb5b9d245b8aca558eb