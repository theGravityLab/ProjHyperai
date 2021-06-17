pipeline {
  agent any
  stages {
    stage('Restore') {
      steps {
        sh 'dotnet restore'
      }
    }

    stage('Build') {
      steps {
        sh 'dotnet publish -r linux-x64 ./HyperaiShell/HyperaiShell.App'
      }
    }

    stage('Prepare Enviroment') {
      steps {
        sh '''rm %WORKSPACE%/HyperaiShell/*.dll
rm %WORKSPACE%/HyperaiShell/*.so
rm %WORKSPACE%/HyperaiShell/HyperaiShell.App'''
      }
    }

    stage('Publish to Production') {
      steps {
        sh 'cp %WORKSPACE%/HyperaiShell/HyperaiShell.App/bin/debug/net5.0/linux-x64/publish/* /home/app/HyperaiShell'
      }
    }

  }
}