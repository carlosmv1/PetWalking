const clients = [
  { id: 1, name: "Juan Pérez", username: "juanp", email: "juan@example.com" },
  { id: 2, name: "Lucía Gómez", username: "lucia.g", email: "lucia@example.com" },
  { id: 3, name: "Carlos Rivas", username: "crivas", email: "carlos@pets.com" }
];

function renderClientList() {
  const nameFilter = document.getElementById('searchName').value.toLowerCase();
  const usernameFilter = document.getElementById('searchUsername').value.toLowerCase();
  const tbody = document.getElementById('clientTableBody');
  tbody.innerHTML = '';

  clients
    .filter(client =>
      client.name.toLowerCase().includes(nameFilter) &&
      client.username.toLowerCase().includes(usernameFilter)
    )
    .forEach(client => {
      const row = `
        <tr class="border-t">
          <td class="px-4 py-2">${client.name}</td>
          <td class="px-4 py-2">${client.username}</td>
          <td class="px-4 py-2">${client.email}</td>
          <td class="px-4 py-2">
          <button
            onclick="loadPets(1)"
            class="bg-blue-500 text-white px-3 py-1 rounded hover:bg-blue-600 text-sm mr-2"
          >
            pets
          </button>
          </td>
        </tr>
      `;
      tbody.innerHTML += row;
    });
}

function loadPets(clientId) {
  loadContent('clients/pets');
  history.pushState({}, '', `#clients/pets?id=${clientId}`);
  sessionStorage.setItem('currentClientId', clientId); // opcional para acceder desde JS
}

// Asegura que los inputs existan antes de agregar eventos
setTimeout(() => {
  const nameInput = document.getElementById('searchName');
  const usernameInput = document.getElementById('searchUsername');

  if (nameInput && usernameInput) {
    nameInput.addEventListener('input', renderClientList);
    usernameInput.addEventListener('input', renderClientList);
  }

  renderClientList();
}, 0); // Espera 0ms para asegurar que el DOM se haya insertado
