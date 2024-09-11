let userToken = null;

// Login form submission handling
document.getElementById('login-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    // Simulate login request to server
    const response = await fetch('http://localhost:5244/user', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    });

    const data = await response.json();

    if (data.success) {
        userToken = data.token;
        document.getElementById('auth-section').style.display = 'none';
        document.getElementById('survey-section').style.display = 'block';

        // Check if the user has already submitted the survey
        checkIfAlreadySubmitted();
    } else {
        alert('Invalid login credentials.');
    }
});

// Survey form submission handling
document.getElementById('survey-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    // Gather survey data
    const q1 = document.querySelector('input[name="q1"]:checked')?.value;
    const q2 = document.querySelector('input[name="q2"]:checked')?.value;
    const q3 = document.getElementById('q3').value;
    const q4 = document.getElementById('q4').value;

    // Submit survey data with user token
    //****TODO: voir comment on envoit le data****
    const response = await fetch('https://your-backend-api.com/submit-survey', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${userToken}`
        },
        body: JSON.stringify({ q1, q2, q3, q4 })
    });

    const data = await response.json();

    if (data.success) {
        alert('Survey submitted successfully! Thank you.');
    } else {
        alert('Error submitting survey. Please try again.');
    }
});

// Check if user has already submitted survey
//**TODO: regarder si isActive est a true ou a false**
async function checkIfAlreadySubmitted() {
    // const response = await fetch('https://your-backend-api.com/check-submission', {
    //     method: 'GET',
    //     headers: { 'Authorization': `Bearer ${userToken}` }
    // });

    const data = await response.json();

    if (data.alreadySubmitted) {
        document.getElementById('survey-form').style.display = 'none';
        document.getElementById('already-submitted').style.display = 'block';
    }
}
