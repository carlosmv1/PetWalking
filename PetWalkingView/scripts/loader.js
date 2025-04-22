function loadContent(pageWithQuery) {
  const [page] = pageWithQuery.split("?");

  // ✅ Actualiza el hash en la barra del navegador
  window.location.hash = pageWithQuery;

  fetch(`pages/${page}.html`)
    .then((res) => res.text())
    .then((html) => {
      document.getElementById("content").innerHTML = html;

      const scriptMap = {
        "clients/create-pet": "../scripts/pages/clients/create-pet.js",
        "clients/edit-pet": "../scripts/pages/clients/edit-pet.js",
        "clients/clients": "../scripts/pages/clients/clients.js",
        "clients/pets": "../scripts/pages/clients/pets.js",
        "users/create-user": "../scripts/pages/users/create-users.js",
        "users/edit-user": "../scripts/pages/users/edit-user.js",
        "walkers/walkers": "../scripts/pages/walkers/walkers.js",
        "walkers/calendar": "../scripts/pages/walkers/calendar.js",
      };

      if (scriptMap[page]) {
        const script = document.createElement("script");
        script.src = scriptMap[page];
        document.body.appendChild(script);
      }
    })
    .catch((err) => {
      document.getElementById(
        "content"
      ).innerHTML = `<p class="text-red-500">Error loading page: ${err}</p>`;
    });
}
window.addEventListener("hashchange", () => {
  const hash = window.location.hash.slice(1); // elimina el #
  if (hash) {
    loadContent(hash);
  }
});

// Carga inicial
window.onload = () => loadContent("data");
