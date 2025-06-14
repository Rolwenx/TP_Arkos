# 🌍 Projet Unity – Multi-environnements interactifs

**Auteur** : Yoke NGASSA 
**Version Unity** : Unity 6000.0.47f URP  
**Scènes principales** : `MainScene`, `TerrainProcedurale`

---

## 🎮 Présentation

Ce projet Unity est une scène interactive regroupant **trois environnements distincts** :  
- ❄️ Zone Neige  
- 🌲 Zone Forêt  
- 🌋 Zone Volcan  

Chaque zone met en œuvre les compétences développées au fil des TPs du semestre :  
Shader Graph, Particules, Timeline, NavMesh, Animation, Pathfinding, etc.

---

## 🧊 Zone Neige

- Tempête de neige via Shader Graph + particules
- Dialogue interactif avec un PNJ animé (animation + rigging)
- Spawn aléatoire d’un **bouton** : le joueur doit le trouver pour calmer la tempête
- La caméra cinématique se déclenche automatiquement
- Le bouton débloque l’accès aux zones suivantes

🎓 TPs utilisés :
- TP5 Shader Graph : shader neige au sol et sur les murs
- TP6 Particules : effet 1 - tempête de neige
- TP7 Animation + interaction : PNJ, timeline, caméra

---

## 🌲 Zone Forêt

- Timer de 1 minute au démarrage
- Zones de **brume toxique** (particules + damage zone)
- Le joueur meurt après 3 expositions
- Mare avec shader d’eau
- Feu de camp qui s’allume automatiquement quand le joueur approche
- Un bouton caché désactive les toxines

🎓 TPs utilisés :
- TP5 : Shader d’herbe + eau animée
- TP6 : effet 2 (feu de camp qui s'enflamme à proximité du joueur), effet de brume (effet 3), effet 1 (feuillage)
- TP7 : déclenchement FX via trigger + événements

---

## 🌋 Zone Volcan

- Le robot doit atteindre une station de secours à travers un terrain dangereux
- Un générateur désactivable retire dynamiquement une barrière énergétique (via NavMeshSurface.BuildNavMesh()) et démare le chemin du robot
- Obstacles dynamiques.

🎓 TPs utilisés :
- TP8 Pathfinding réactif
- Utilisation avancée de NavMeshAgent, Obstacle, Surface
- Animation réactive liée à la vélocité de l'agent

---

## 🌄 Scène secondaire : TerrainProcedurale
---
## 💬 Commentaires

Ce projet est une synthèse créative et technique des TPs réalisés dans le semestre, sous la tutelle de Mr. Guillaume LOUP.
Chaque environnement met en valeur une compétence : shaders, FX, pathfinding, animation.
L’ensemble est pensé comme un parcours interactif évolutif, intégrant narration, gameplay et progression technique.



