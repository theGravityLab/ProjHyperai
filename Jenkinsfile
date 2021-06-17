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
        sh '''rm ~/HyperaiShell/*.dll
rm ~/HyperaiShell/*.so
rm ~/HyperaiShell/HyperaiShell.App'''
      }
    }

    stage('Publish to Production') {
      steps {
        sh 'cp ./HyperaiShell/HyperaiShell.App/bin/debug/net5.0/linux-x64/publish/* ~/HyperaiShell'
      }
    }

  }
}