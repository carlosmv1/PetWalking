document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("contactForm");
  
    if (!form) {
      console.error("Formulario no encontrado");
      return;
    }
  
    form.addEventListener("submit", async function (e) {
      e.preventDefault();
  
      const formData = new FormData(form); // 👈 asegúrate que esto es el <form>
      const data = {
        name: formData.get("name"),
        email: formData.get("email"),
        message: formData.get("message")
      };
  
      try {
        const response = await fetch("http://localhost:5140/api/contact/send-email", {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(data)
        });
  
        const result = await response.json();
        const statusEl = document.getElementById("contactStatus");
  
        if (response.ok) {
            statusEl.textContent = "Mensaje enviado correctamente 🐶📧";
            statusEl.classList.remove("opacity-0", "text-red-600");
            statusEl.classList.add("opacity-100", "text-green-600");
          
            form.querySelectorAll("input, textarea").forEach(field => field.value = "");
          
          } else {
            statusEl.textContent = "Error al enviar el mensaje 😢";
            statusEl.classList.remove("opacity-0", "text-green-600");
            statusEl.classList.add("opacity-100", "text-red-600");
          }
          
          // Ocultar mensaje automáticamente después de 4 segundos
          setTimeout(() => {
            statusEl.classList.add("opacity-0");
          }, 4000);
      } catch (err) {
        console.error("Error en la solicitud:", err);
      }
    });
  });
  