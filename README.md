# TranslationManagement

This app is designed to help manage translators and the jobs they work on.

## How to Run

### Using Docker

1. **Prerequisites:** Ensure Docker is installed on your system. You can download it from [the official Docker website](https://www.docker.com/products/docker-desktop/).

2. **Launch the Backend:**
    - Navigate to the backend project folder (`\InterviewTestProject.TranslationManagement-master`).
    - Build the Docker image with the following command:
        ```bash
        docker build -f Dockerfile -t translation-management ..
        ```
    - Run the Docker container using the command:
        ```bash
        docker run -p 7729:80 translation-management
        ```

3. **Launch the Frontend:**
    - Navigate to the frontend project folder (`\frontend`).
    - Build the Docker image with the command:
        ```bash
        docker build -t translation-management-front .
        ```
    - Run the Docker container interactively with port mapping:
        ```bash
        docker run -it -p 3000:3000 translation-management-front
        ```
