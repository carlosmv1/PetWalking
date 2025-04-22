// Obtener el ID del usuario desde la URL (ej: edit-users.html?id=1)
const urlParams = new URLSearchParams(window.location.search);
const userId = urlParams.get('id');

// Si hay un ID válido, cargar los datos del usuario
if (userId) {
  fetch(`/api/user/${userId}`)
    .then(res => {
      if (!res.ok) throw new Error("User not found");
      return res.json();
    })
    .then(user => {
      // Asignar valores a los campos del formulario
      document.getElementById('user_id').value = user.user_id;
      document.getElementById('user_name').value = user.user_name;
      document.getElementById('first_name').value = user.first_name || '';
      document.getElementById('last_name').value = user.last_name || '';
      document.getElementById('address').value = user.address || '';
      document.getElementById('city').value = user.city || '';
      document.getElementById('phone').value = user.phone || '';
      document.getElementById('alt_phone').value = user.alt_phone || '';
      document.getElementById('email').value = user.email || '';
      document.getElementById('user_type_id').value = user.user_type_id;
      document.getElementById('zone_id').value = user.zone_id;

      // Mostrar imagen actual o avatar por defecto
      const imgElement = document.getElementById('profilePreview');
      imgElement.src = user.image && user.image !== '' 
        ? user.image 
        : 'https://cdn-icons-png.flaticon.com/512/149/149071.png';
    })
    .catch(err => {
      alert("Failed to load user: " + err);
    });
} else {
  alert("No user ID provided in URL");
}

// Manejo del submit del formulario
const form = document.getElementById('editUserForm');
form.addEventListener('submit', function (e) {
  if (!form.checkValidity()) {
    form.reportValidity();
    return;
  }

  e.preventDefault();
  const formData = new FormData(this);

  fetch(`/api/user/${userId}`, {
    method: 'POST', // o 'PUT' si tu backend está preparado para ello
    body: formData
  })
    .then(res => res.ok ? res.json() : Promise.reject('Update failed'))
    .then(data => {
      alert('User updated successfully');
      // loadContent('users/users'); // si usas carga dinámica
    })
    .catch(err => alert(err));
});

// Función para mostrar preview de imagen cargada
function previewImage(event) {
  const input = event.target;
  const preview = document.getElementById('profilePreview');

  if (input.files && input.files[0]) {
    const reader = new FileReader();
    reader.onload = function (e) {
      preview.src = e.target.result;
    };
    reader.readAsDataURL(input.files[0]);
  }
}
