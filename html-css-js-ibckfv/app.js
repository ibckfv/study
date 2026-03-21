const btn = document.getElementById("generateBtn");
const ideaBox = document.getElementById("idea");

  async function fetchIdea() {
    const proxy = "https://api.allorigins.win/raw?url=";
    const url = proxy + encodeURIComponent("https://bored-api.appbrewery.com/random");

    try {
      const res = await fetch(url);
      const data = await res.json();
      //console.log("JSON:", data);

      ideaBox.innerHTML = `
        <p><strong>${data.activity}</strong></p>
        <p>Тип: ${data.type}</p>
        <p>Участников: ${data.participants}</p>
        <p>Стоимость: ${data.price*100}$</p>
      `;
    } catch (err) {
      //console.error("Ошибка:", err);
      ideaBox.innerHTML = "<p>Ошибка при загрузке :(</p>";
    }
  }

btn.addEventListener("click", fetchIdea);