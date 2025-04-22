// scripts/pages/clients/edit-pet.js

// Obtener parámetros desde la URL (ej: ?id=2&client=1)
const urlParams = new URLSearchParams(window.location.search);
const petId = urlParams.get('id');
const clientId = urlParams.get('client');

document.getElementById('pet_id').value = petId;
document.getElementById('client_id').value = clientId;

// Simular carga de datos
const mockPets = [
  { pet_id: '1', name: 'Bobby', breed: 'Golden', age: 3, color: 'Beige' },
  { pet_id: '2', name: 'Mia', breed: 'Poodle', age: 5, color: 'White' }
];

const pet = mockPets.find(p => p.pet_id === petId);
if (pet) {
  document.getElementById('name').value = pet.name;
  document.getElementById('breed').value = pet.breed;
  document.getElementById('age').value = pet.age;
  document.getElementById('color').value = pet.color;
}

// Manejar envío del formulario
const form = document.getElementById('editPetForm');
form.addEventListener('submit', function (e) {
  e.preventDefault();

  const formData = new FormData(form);
  console.log('Pet updated:', Object.fromEntries(formData));
  alert(`Pet "${formData.get('name')}" updated successfully.`);

  // Opcional: redirigir a lista de mascotas del cliente
  // loadContent(`clients/pets?id=${clientId}`);
});

function previewPetImage(event) {
    const input = event.target;
    const preview = document.getElementById('petPreview');
  
    if (input.files && input.files[0]) {
      const reader = new FileReader();
      reader.onload = function (e) {
        preview.src = e.target.result;
      };
      reader.readAsDataURL(input.files[0]);
    }
  }