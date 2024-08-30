# NegoSoft

# Instructions de Configuration

Pour configurer l'application, vous devez créer un fichier `.env` à la racine de la solution et y ajouter les variables d'environnement nécessaires. Suivez les étapes ci-dessous pour configurer correctement votre environnement :

## Étapes à suivre

1. **Créer le fichier `.env`**

   À la racine de votre solution, créez un fichier nommé `.env`. Ce fichier ne doit pas être suivi par Git (il est inclus dans `.gitignore` pour éviter de partager des informations sensibles).

   ```bash
   touch .env
   
# Exemple de variables d'environnement
DB_CONNECTION_STRING= the connection string 
