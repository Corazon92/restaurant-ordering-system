# Restaurant Ordering System — C# WinForms

> Application de borne de commande développée en C# WinForms avec catalogue et facturation basés sur Excel.  
> **English version below.**

## Fonctionnement

Le projet gère les catégories **Menu, Plat, Boisson et Dessert**. Les produits sont chargés depuis un classeur Excel avec leur catégorie, nom, prix et référence d'image.

L'interface génère dynamiquement les produits puis permet :
- ajout au panier ;
- gestion des quantités ;
- diminution/modification ;
- calcul du total ;
- validation de commande ;
- génération/complétion d'une facture Excel avec numéro, date et heure ;
- accès à une interface administrateur ;
- ajout et suppression de produits.

La traduction anglaise évoquée dans le projet historique n'est **pas présentée comme terminée**.

## Portabilité et nettoyage

L'archive originale dépendait de chemins absolus vers un ancien dossier utilisateur/OneDrive. Avant le **premier commit public des sources**, ces références ont été remplacées par des chemins relatifs :

- `data/products.xlsx`
- `data/invoice.xlsx`

L'ancien secret administrateur codé en dur a également été supprimé **avant publication**. L'authentification lit désormais `RESTAURANT_ADMIN_PASSWORD` depuis l'environnement.

Aucune valeur historique du secret n'est présente dans ce dépôt.

## Technologies

**C# · .NET / WinForms · Microsoft Office Interop Excel · Visual Studio**

## Structure

```text
src/
├── Program.cs
├── Menu.cs
├── Plat.cs
├── Boisson.cs
├── Dessert.cs
├── Client.cs
└── appli.cs
data/
└── README.md
```

Les fichiers Designer, ressources WinForms et le projet Visual Studio récupérés sont maintenant inclus. Les chemins locaux et l'ancien mot de passe administrateur ont été nettoyés avant publication.

## Utilisation

Le code attend Microsoft Excel/Office Interop comme dans le projet d'origine. Les chemins de données sont maintenant locaux au projet. Pour l'accès administrateur, définir la variable d'environnement `RESTAURANT_ADMIN_PASSWORD`. Le projet Visual Studio se trouve dans `RestaurantOrderingSystem.csproj`.

## Limites

Le projet historique utilisait Excel comme stockage applicatif. Cette architecture est adaptée à l'exercice pédagogique mais une application moderne utiliserait plutôt une base de données ou un format local indépendant d'Office.

---

# English version

C# WinForms restaurant-ordering kiosk using Excel-backed product data and invoicing.

Verified features include Menu/Dish/Drink/Dessert categories, dynamic product loading, cart quantities, total calculation, order validation, invoice data, order number/date/time and an administration interface for product management.

Before the **first public source commit**, historical absolute OneDrive paths were replaced with project-relative data paths and the hard-coded administrator secret was removed. Admin access now reads `RESTAURANT_ADMIN_PASSWORD` from the environment.

The English UI translation from the historical project is **not claimed as complete**.
