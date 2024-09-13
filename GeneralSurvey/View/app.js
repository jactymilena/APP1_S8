let userToken = null;
let questionsData = {};

/*Login section*/
document.addEventListener('DOMContentLoaded', () => {
    const loginForm = document.getElementById('login-form');

    loginForm.addEventListener('submit', (event) => {
        event.preventDefault();

        // Simuler la validation du formulaire
        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;

        // Vous devriez ajouter la logique pour vérifier les informations d'identification ici.
        // Par exemple, vous pouvez envoyer une requête POST au serveur avec fetch.
        
        // Si la connexion est réussie
        if (username && password) { // Remplacez ceci par une vérification réelle
            window.location.href = 'section.html';
        } else {
            // Afficher un message d'erreur ou gérer la connexion échouée
            alert('Invalid login credentials.');
        }
    });
});

/*CHOICE section*/
document.addEventListener('DOMContentLoaded', () => {
    const survey1Button = document.getElementById('survey1-button');
    const survey2Button = document.getElementById('survey2-button');

    survey1Button.addEventListener('click', () => {
        window.location.href = 'survey.html?survey=1'; // Redirige vers survey.html avec le paramètre survey=1
    });

    survey2Button.addEventListener('click', () => {
        window.location.href = 'survey.html?survey=2'; // Redirige vers survey.html avec le paramètre survey=2
    });
});

// Fonction pour obtenir les paramètres d'URL (1 ou 2)
function getQueryParam(param) {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(param);
}

// Fonction pour charger le contenu du sondage
async function loadSurveyContent(surveyId) {
    try {
        // Charger le fichier JSON
        const response = await fetch('/../sondage.json');
        const data = await response.json();

        // Vérifier si les données du sondage existent
        if (data[`Sondage_${surveyId}`]) {
            const survey = data[`Sondage_${surveyId}`];
            const surveyTitle = document.getElementById('survey-title');
            const surveyContent = document.getElementById('survey-content');

            // Mettre à jour le titre et le contenu
            surveyTitle.textContent = `Sondage ${surveyId}`;
            surveyContent.innerHTML = survey.questions.map(q => `
                <div>
                    <p>${q.question}</p>
                    ${Object.entries(q.options).map(([key, value]) => `
                        <div>
                            <input type="radio" id="${key}" name="${q.question}" value="${key}">
                            <label for="${key}">${value}</label>
                        </div>
                    `).join('')}
                </div>
            `).join('');
        } else {
            // Gérer le cas où le sondage n'existe pas
            document.getElementById('survey-title').textContent = 'Survey Not Found';
            document.getElementById('survey-content').innerHTML = '<p>Sorry, the survey you are looking for does not exist.</p>';
        }
    } catch (error) {
        console.error('Error loading survey data:', error);
        document.getElementById('survey-title').textContent = 'Error';
        document.getElementById('survey-content').innerHTML = '<p>Unable to load survey data.</p>';
    }
}

    // Charger le contenu du sondage en fonction du paramètre d'URL
    document.addEventListener('DOMContentLoaded', () => {
        const surveyId = getQueryParam('survey');
        loadSurveyContent(surveyId);
    });

// // Check if the user has already submitted the selected survey
// //**TODO: regarder si isActive est a true ou a false**
// async function checkIfAlreadySubmitted(surveyNumber) {
//     const response = await fetch(`https://your-backend-api.com/check-survey?survey=${surveyNumber}`, {
//         headers: { 'Authorization': `Bearer ${userToken}` }
//     });
    
//     const data = await response.json();

//     if (data.alreadySubmitted) {
//         if (surveyNumber === 1) {
//             document.getElementById('already-submitted').style.display = 'block';
//             document.getElementById('survey1-form').style.display = 'none';
//         } else if (surveyNumber === 2) {
//             document.getElementById('already-submitted2').style.display = 'block';
//             document.getElementById('survey2-form').style.display = 'none';
//         }
//     }
// }
