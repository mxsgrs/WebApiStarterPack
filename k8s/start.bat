:: Expenses
kubectl apply -f .\expenses\expenses-deployment.yaml
kubectl apply -f .\expenses\expenses-service.yaml

:: Settings
kubectl apply -f .\settings\settings-deployment.yaml
kubectl apply -f .\settings\settings-service.yaml

:: User
kubectl apply -f .\user\user-deployment.yaml
kubectl apply -f .\user\user-service.yaml

:: Ingress-Nginx Controller
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.11.2/deploy/static/provider/cloud/deploy.yaml
timeout /t 30
kubectl apply -f .\ingress\nginx-ingress.yaml

set /p DUMMY=Hit enter to continue...