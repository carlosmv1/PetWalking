// Obtener clientId de la URL
const urlParams = new URLSearchParams(window.location.search);
const clientId = urlParams.get('id');
document.getElementById('petClientId').textContent = clientId;

// Simulación de mascotas del cliente
let pets = [
  { pet_id: 1, name: "Bobby", breed: "Golden Retriever", age: 3 },
  { pet_id: 2, name: "Mia", breed: "Poodle", age: 5 }
];

// Renderizar lista de mascotas function renderPetList() {
  const tbody = document.getElementById('petTableBody');
  tbody.innerHTML = '';

  pets.forEach(pet => {
    const row = `
      <tr class="border-t">
        <td class="px-4 py-2">${pet.name}</td>
        <td class="px-4 py-2">${pet.breed}</td>
        <td class="px-4 py-2">${pet.age}</td>
        <td class="px-4 py-2">
          <button onclick="editPet(${pet.pet_id})" class="bg-blue-500 text-white px-3 py-1 rounded hover:bg-blue-600 text-sm mr-2">Edit</button>
          <button onclick="deletePet(${pet.pet_id})" class="bg-red-500 text-white px-3 py-1 rounded hover:bg-red-600 text-sm">Delete</button>
        </td>
      </tr>
    `;
    tbody.innerHTML += row;
  });

  function editPet(petId) {
    // Incluye también el ID del cliente actual
    loadContent(`clients/edit-pet?id=${petId}&client_id=${clientId}`);
  }

// Eliminar mascota
function deletePet(id) {
  if (confirm("Are you sure you want to delete this pet?")) {
    pets = pets.filter(p => p.pet_id !== id);
    renderPetList();
  }
}

// Inicializar
renderPetList();