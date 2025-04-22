document.addEventListener("DOMContentLoaded", function () {
  const form = document.getElementById("loginForm");
  const result = document.getElementById("loginResult");

  form.addEventListener("submit", async function (e) {
    e.preventDefault();

    const userName = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    try {
      const response = await fetch("http://localhost:5140/api/auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({ userName, password })
      });

      if (response.ok) {
        const data = await response.json();
        result.textContent = `✅ Login exitoso. Bienvenido, ${data.firstName || data.userName}!`;
        result.style.color = "green";
      } else if (response.status === 401) {
        result.textContent = "❌ Usuario o contraseña incorrectos.";
        result.style.color = "red";
      } else {
        result.textContent = "❌ Error inesperado en el servidor.";
        result.style.color = "red";
      }
    } catch (error) {
      console.error("Error al conectar con el backend:", error);
      result.textContent = "❌ No se pudo conectar al servidor.";
      result.style.color = "red";
    }
  });
});
