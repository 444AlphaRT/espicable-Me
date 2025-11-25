# 🎮 Espicable Me – Game Project

**מגישים:**  
- רעות בכור  
- רון אמסלם  
- תומר גל 

**קישור למאגרים של הצוות:**  
🔗 https://github.com/orgs/444AlphaRT/repositories
https://alpha444rt.itch.io/espicable-me

---

## 📘 תיאור המשחק
המשחק מבוסס על דמותו של *גרו* מהסרט "גנוב על הירח", ונותן לשחקן לחוות שלב קצר מתוך חייו — בין עולם הרשע התיאטרלי שלו לבין החיים המשפחתיים החדשים שנכנסו אליו.

במהלך המשחק השחקן צריך:
- לשוטט ברחוב
- לאסוף מיניונים (F)
- להחזיר אותם לביתו של גרו
- לשחרר אותם בבית
- להשלים את איסוף כל 5 המיניונים

כל מיניון נאסף פעם אחת, ורק אחד לכל יציאה לרחוב — מה שמכניס שכבה קטנה של ניהול משאבים והתקדמות.

---

## 🎯 מטרת המשחק
לאסוף **5 מיניונים** ברחוב, להיכנס לבית של גרו, ולהשלים את השלב.  
לאחר שהכול נאספו → ניתן ללחוץ **R** ולהתחיל את המשחק מחדש.

---

## 🕹️ איך משחקים?

### 🌆 באזור הרחוב (Hub Scene)
- תנועה: **WASD / Arrow Keys**  
- איסוף מיניון: **F** (בתוך טווח קצר)  
- כניסה לבית: נגיעה בדלת  

כל יציאה לרחוב מאפשרת איסוף של **מיניון אחד בלבד**.

---

### 🏠 בבית של גרו (Home Scene)
- אם אספת מיניון ברחוב → לחץ **F** כדי להניח אותו בבית  
- מיניון שהונח בבית מופיע **במיקום רנדומלי**  
- לאחר איסוף כל 5 המיניונים → מופיעה הודעת סיום  
- לחיצה על **R** → מאפסת את המשחק ומחזירה לרחוב

---

## 🧩 מבנה המערכת (System Overview)
המשחק מורכב ממספר רכיבים מרכזיים:

- **ResourceManager** – מנהל את מצב המשחק בין הסצנות (Singleton)  
- **GruStreetController** – שולט על התנועה של גרו  
- **MinionSpawner** – מחולל מיניונים ברחוב  
- **MinionHomeSpawner** – מציב מיניונים שנאספו בתוך הבית  
- **MinionStreetCollectible** – לוגיקת איסוף המיניון  
- **HubSceneManager** – מאפס את כמות האיסוף בסצנת הרחוב  
- **HomeLevelEndController** – מזהה סיום ו־Restart  

---

## 🖼️ UML — תרשים מחלקות מלא

```mermaid
classDiagram
    class ResourceManager {
        +static ResourceManager Instance
        +bool[] collectedMinions
        +int totalMinionsToWin
        +int totalCollectedMinions
        +int collectedThisRun
        +void ResetRunCounter()
        +void ResetAllMinions()
        +bool HasCollectedAllMinions()
        +bool TryCollectMinion(int minionId)
    }

    class GruStreetController {
        +float moveSpeed
        +void Update()
        -void HandleMovement()
    }

    class MinionStreetCollectible {
        +int minionId
        +float collectDistance
        -Transform player
        +void Start()
        +void Update()
    }

    class MinionSpawner {
        +GameObject minionPrefab
        +int minionsToSpawn
        +float margin
        +void Start()
        -void SpawnMinions()
    }

    class MinionHomeSpawner {
        +GameObject homeMinionPrefab
        +float margin
        +void Start()
        -void SpawnHomeMinions()
    }

    class HubSceneManager {
        +void Start()
    }

    class HomeLevelEndController {
        +string hubSceneName
        -bool canRestart
        +void Start()
        +void Update()
    }

    ResourceManager <.. GruStreetController
    MinionStreetCollectible --> ResourceManager
    MinionSpawner --> MinionStreetCollectible
    MinionHomeSpawner --> ResourceManager
    HubSceneManager --> ResourceManager
    HomeLevelEndController --> ResourceManager
