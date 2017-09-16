# Mise à Jour EPG sous Windows

A l'heure actuelle cela ne fonctionne plus... j'essaierais plus tard de le mettre à jour pour que ça refonctionne, en fait le fichier xml tout fait sur le net, n'est plus mis à jour depuis le 20 août.

J'utilise Kodi pour regarder la TV, avec l'Add-On "PVR Simple IPTV",
et il y a la possiblité d'ajouter les programmes TV sous format XML.

Le site internet http://xmltv.dtdns.net/ fournit un fichier XML
compressé en Zip qui fournit l'EPG pour la France.

J'ai donc écrit un morceau de code en C# qui télécharge le fichier
et le décompresse pour qu'il soit lisible avec Kodi.

Il faut dotNet Framework 4.5, installé sur le PC pour que ça fonctionne.

Le fichier XML fonctionne pour tout logiciel utilisant l'EPG.

Dans l'extention de Kodi : "PVR Simple IPTV", choisir playlist local et choisir le fichiers m3u du dépot (si vous êtes chez Free, sinon faudra éditer votre propre fichier m3u), choisir xml local (après avoir exécuter l'exe) le fichier xml se situe dans "documents\EPG" et pour les logos choisir logos locaux et choisir le dossier créer après avoir dézippé le fichier "logo.zip"
