
# How to deploy the API service in kubernetes cluster

## Cluster running on a VM

The procedure below implies that a VM is deployed with [ICAP Server OVA](https://github.com/k8-proxy/glasswall-servers-eval/wiki) image

```
    ssh centos@<IP address of the VM>
    password:
```


Run the following command sequence in the SSH terminal:

``` 
    wget https://raw.githubusercontent.com/k8-proxy/k8-rebuild-rest-api/issue_15_use_sow_cdr_engine/kubernetes/api-service.yaml
    kubectl create ns rebuild-rest-api
    kubectl apply -n rebuild-rest-api -f api-service.yaml
    kubectl port-forward -n rebuild-rest-api --address 0.0.0.0 service/rebuild-rest-api 8888:80
```

Send file rebuild request to ```http://<IP address of the VM>:8888/api/rebuild/file```
