document.getElementById('loginForm').addEventListener('submit', async e => {
    e.preventDefault();
    const fd = new FormData(e.target);
    const body = { usernameOrEmail: fd.get('identifier'), password: fd.get('password') };
    const res = await fetch('/api/login',{ method: 'POST', headers: {'Content-Type':'application/json'}, body: JSON.stringify(body) });
    const j = await res.json();
    if (j.ok) {
        localStorage.setItem('token', j.token);
        document.getElementById('msg').textContent = "Logged in";
    } else document.getElementById('msg').textContent = j.error || JSON.stringify(j);
});