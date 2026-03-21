async function loadAds(){
    const res = await fetch('/api/ads');
    const ads = await res.json();
    const c = document.getElementById('ads');
    c.innerHTML = ads.map(a => `<div><h3>${a.title}</h3><p>${a.description||''}</p><b>${a.price||''}</b></div>`).join('');
}
document.getElementById('adForm').addEventListener('submit', async e=>{
    e.preventDefault();
    const token = localStorage.getItem('token');
    if (!token) { alert('Please login'); return; }
    const fd = new FormData(e.target);
    const body = { title: fd.get('title'), description: fd.get('description'), price: parseFloat(fd.get('price') || 0) };
    const res = await fetch('/api/ads', { method: 'POST', headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json' }, body: JSON.stringify(body) });
    if (!res.ok) alert(await res.text()); else { alert('Created'); loadAds(); }
});
loadAds();