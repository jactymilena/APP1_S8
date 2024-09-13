let userToken = null;
let questionsData = {};

/*Login section*/
document.addEventListener('DOMContentLoaded', () => {
    const loginForm = document.getElementById('login-form');

    loginForm.addEventListener('submit', (event) => {
        event.preventDefault(); // Empêche l'envoi du formulaire

        // Simuler la validation du formulaire
        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;

        // Vous devriez ajouter la logique pour vérifier les informations d'identification ici.
        // Par exemple, vous pouvez envoyer une requête POST au serveur avec fetch.
        
        // Si la connexion est réussie
        if (username && password) { // Remplacez ceci par une vérification réelle
            // Masquer la section de connexion et afficher la section de choix de l'enquête ou transfert vers le bon url
            window.location.href = 'section.html';
        } else {
            // Afficher un message d'erreur ou gérer la connexion échouée
            alert('Nom d\'utilisateur ou mot de passe incorrect.');
        }
    });
});

/*CHOICE section*/
// choix de selection of Survey 1
// document.getElementById('survey1-button').addEventListener('click', function() {
//     document.getElementById('survey1-section').style.display = 'block';
// });

// // choix the selection of Survey 2
// document.getElementById('survey2-button').addEventListener('click', function() {
//     document.getElementById('survey2-section').style.display = 'block';
// });

/*CHOICE section*/
// document.addEventListener('DOMContentLoaded', () => {
//     const survey1Button = document.getElementById('survey1-button');
//     const survey2Button = document.getElementById('survey2-button');

//     survey1Button.addEventListener('click', () => {
//         window.location.href = 'survey.html?survey=1'; // Redirige vers survey.html avec le paramètre survey=1
//     });

//     survey2Button.addEventListener('click', () => {
//         window.location.href = 'survey.html?survey=2'; // Redirige vers survey.html avec le paramètre survey=2
//     });
// });

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
