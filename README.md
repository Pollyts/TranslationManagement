# TranslationManagement
This app should help to manage translators and jobs they work on. 

# How to run

Using Docker:
1. Docker must be installed https://www.docker.com/products/docker-desktop/
2. Launch the backend: 
    2.1. Go to the folder with backend project (folder \InterviewTestProject.TranslationManagement-master)
    2.2. Run the command:
    docker build -f Dockerfile -t translation-management ..
    2.3. Run the command:
    docker run -p 7729:80 translation-management
3. Launch the frontend: 
    3.1. Go to the folder with frontend project (folder \frontend)
    3.2. Run the command:
    docker build -t translation-management-front .
    3.3. Run the command:
    docker run -it -p 3000:3000 translation-management-front