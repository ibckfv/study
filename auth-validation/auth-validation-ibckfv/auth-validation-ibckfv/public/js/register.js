const form = document.getElementById('regForm');
form.addEventListener('submit', async (e) => {
    e.preventDefault();
    document.getElementById('errors').textContent = '';
    document.getElementById('success').textContent = '';

    const fd = new FormData(form);
    const data = {
        username: fd.get('username'),
        email: fd.get('email'),
        firstName: fd.get('firstName'),
        lastName: fd.get('lastName'),
        phone: fd.get('phone'),
        dob: fd.get('dob'),
        password: fd.get('password'),
        confirmPassword: fd.get('confirmPassword')
    };

    // client-side extra checks
    const errors = [];
    if (data.password.length < 8) errors.push('Password too short');
    if (data.password !== data.confirmPassword) errors.push('Passwords mismatch');
    if (!/\d/.test(data.password)) errors.push('Password needs digit');
    if (!/[A-Z]/.test(data.password)) errors.push('Password needs uppercase');
    if (!/[!@#\$%\^&\*]/.test(data.password)) errors.push('Password needs special char');
    if (errors.length) { document.getElementById('errors').textContent = errors.join('; '); return; }

    const res = await fetch('/api/register', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(data) });
    const j = await res.json();
    if (!res.ok) { document.getElementById('errors').textContent = JSON.stringify(j); }
    else document.getElementById('success').textContent = 'Registered successfully';
});