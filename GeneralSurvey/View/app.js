
let userToken = null;

/*Login section*/
document.getElementById('login-form').addEventListener('submit', async function(event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    
    // Simulate login request to server
    //***Modifier la requête***
    const response = await fetch('https://your-backend-api.com/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    });
    
    const data = await response.json();
    
    if (data.success) {
        userToken = data.token;
        document.getElementById('auth-section').style.display = 'none';
        document.getElementById('survey-choice-section').style.display = 'block';
    } else {
        alert('Invalid login credentials.');
    }
});

// choix de selection of Survey 1
document.getElementById('survey1-button').addEventListener('click', function() {
    document.getElementById('survey-choice-section').style.display = 'none';
    document.getElementById('survey1-section').style.display = 'block';
    checkIfAlreadySubmitted(1); // regarde si survey number 1
});

// Handling the selection of Survey 2
document.getElementById('survey2-button').addEventListener('click', function() {
    document.getElementById('survey-choice-section').style.display = 'none';
    document.getElementById('survey2-section').style.display = 'block';
    checkIfAlreadySubmitted(2); // Pass survey number 2
});

// Survey 1 form submission handling
document.getElementById('survey1-form').addEventListener('submit', async function(event) {
    event.preventDefault();

    const q1 = document.querySelector('input[name="q1"]:checked')?.value;
    const q2 = document.querySelector('input[name="q2"]:checked')?.value;
    const q3 = document.getElementById('q3').value;
    const q4 = document.getElementById('q4').value;

    //****TODO: voir comment on envoit le data****
    const response = await fetch('https://your-backend-api.com/submit-survey1', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${userToken}`
        },
        body: JSON.stringify({ q1, q2, q3, q4 })
    });
    
    const data = await response.json();

    if (data.success) {
        alert('Survey 1 submitted successfully! Thank you.');
    } else {
        alert('Error submitting survey. Please try again.');
    }
});

// Survey 2 form submission handling
document.getElementById('survey2-form').addEventListener('submit', async function(event) {
    event.preventDefault();

    const q1 = document.querySelector('input[name="q1"]:checked')?.value;
    const q2 = document.querySelector('input[name="q2"]:checked')?.value;
    const q3 = document.getElementById('q3').value;
    const q4 = document.getElementById('q4').value;

    const response = await fetch('https://your-backend-api.com/submit-survey2', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${userToken}`
        },
        body: JSON.stringify({ q1, q2, q3, q4 })
    });
    
    const data = await response.json();

    if (data.success) {
        alert('Survey 2 submitted successfully! Thank you.');
    } else {
        alert('Error submitting survey. Please try again.');
    }
});

// Check if the user has already submitted the selected survey
//**TODO: regarder si isActive est a true ou a false**
async function checkIfAlreadySubmitted(surveyNumber) {
    const response = await fetch(`https://your-backend-api.com/check-survey?survey=${surveyNumber}`, {
        headers: { 'Authorization': `Bearer ${userToken}` }
    });
    
    const data = await response.json();

    if (data.alreadySubmitted) {
        if (surveyNumber === 1) {
            document.getElementById('already-submitted').style.display = 'block';
            document.getElementById('survey1-form').style.display = 'none';
        } else if (surveyNumber === 2) {
            document.getElementById('already-submitted2').style.display = 'block';
            document.getElementById('survey2-form').style.display = 'none';
        }
    }
}
