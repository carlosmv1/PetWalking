const urlParams = new URLSearchParams(window.location.search);
const clientId = urlParams.get('id'); // user_id

// Referencias
const form = document.getElementById('createPetForm');
const userIdInput = document.getElementById('user_id');
const typeSelect = document.getElementById('pet_type_id');
const breedSelect = document.getElementById('pet_breed_id');

// Simulaciones de tipos y razas
const petTypes = [
  { id: 1, name: "Dog" },
  { id: 2, name: "Cat" },
  { id: 3, name: "Bird" }
];

const petBreeds = {
  1: [ { id: 101, name: "Labrador" }, { id: 102, name: "Poodle" } ],
  2: [ { id: 201, name: "Persian" }, { id: 202, name: "Sphynx" } ],
  3: [ { id: 301, name: "Parakeet" }, { id: 302, name: "Canary" } ]
};

// Cargar tipos
function loadPetTypes() {
  typeSelect.innerHTML = '<option value="">Select type</option>';
  petTypes.forEach(type => {
    const option = document.createElement('option');
    option.value = type.id;
    option.textContent = type.name;
    typeSelect.appendChild(option);
  });
}

// Cargar razas según tipo
function loadBreedsForType(typeId) {
  const breeds = petBreeds[typeId] || [];
  breedSelect.innerHTML = '<option value="">Select breed</option>';
  breeds.forEach(breed => {
    const option = document.createElement('option');
    option.value = breed.id;
    option.textContent = breed.name;
    breedSelect.appendChild(option);
  });
}

typeSelect.addEventListener('change', (e) => {
  const selectedTypeId = e.target.value;
  loadBreedsForType(selectedTypeId);
});

// Setear client ID en campo oculto
if (clientId) {
  userIdInput.value = clientId;
} else {
  alert("Client ID not found in URL.");
}

// Capturar submit
form.addEventListener('submit', function (e) {
  e.preventDefault();

  const formData = new FormData(form);
  const pet = {
    name: formData.get('name'),
    gender: formData.get('gender'),
    pet_type_id: formData.get('pet_type_id'),
    pet_breed_id: formData.get('pet_breed_id'),
    observations: formData.get('observations'),
    status: formData.get('status'),
    user_id: formData.get('user_id'),
    image: formData.get('image')?.name || ''
  };

  console.log("🐾 Pet created:", pet);
  alert("Pet created successfully!");

  form.reset();
  breedSelect.innerHTML = '<option value="">Select breed</option>';
});

// Inicializar
loadPetTypes();

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