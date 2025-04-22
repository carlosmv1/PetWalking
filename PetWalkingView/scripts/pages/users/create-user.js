// Asegurar que el formulario exista antes de usarlo
function initCreateUserForm() {
  const form = document.getElementById('userForm');
  if (!form) {
    console.warn('userForm not found.');
    return;
  }

  form.addEventListener('submit', function (e) {
    e.preventDefault();

    const formData = new FormData(form);

    fetch('/api/user', {
      method: 'POST',
      body: formData
    })
      .then(res => res.ok ? res.json() : Promise.reject('Failed to create user'))
      .then(data => {
        alert('User created successfully!');
        form.reset();
      })
      .catch(err => alert(err));
  });
}

initCreateUserForm(); // 👈 se llama cuando el script carga
