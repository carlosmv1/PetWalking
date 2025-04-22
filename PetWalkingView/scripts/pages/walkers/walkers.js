const exampleWalkers = [
    { id: 1, name: "Laura Torres", username: "lauratorres", email: "laura@walkers.com", status: "A" },
    { id: 2, name: "Andrés Ramos", username: "andresr", email: "andres@walkers.com", status: "I" },
    { id: 3, name: "Sara Cruz", username: "sacruz", email: "sara@walkers.com", status: "A" }
  ];
  
  // Render table rows
  function renderWalkerList() {
    const nameFilter = document.getElementById("searchName").value.toLowerCase();
    const usernameFilter = document.getElementById("searchUsername").value.toLowerCase();
    const statusFilter = document.getElementById("searchStatus").value;
  
    const tbody = document.getElementById("walkerTableBody");
    tbody.innerHTML = "";
  
    exampleWalkers
      .filter(walker =>
        walker.name.toLowerCase().includes(nameFilter) &&
        walker.username.toLowerCase().includes(usernameFilter) &&
        (statusFilter === "" || walker.status === statusFilter)
      )
      .forEach(walker => {
        const row = `
          <tr class="border-t">
            <td class="px-4 py-2">${walker.name}</td>
            <td class="px-4 py-2">${walker.username}</td>
            <td class="px-4 py-2">${walker.email}</td>
            <td class="px-4 py-2">
              <button onclick="loadContent('walkers/calendar?id=${walker.id}')" 
                      class="bg-blue-500 text-white px-3 py-1 rounded hover:bg-blue-600 text-sm mr-2">
                Sheduler
              </button>
            </td>
          </tr>
        `;
        tbody.innerHTML += row;
      });
  }
  
  // Eventos de filtro
  document.getElementById("searchBtn").addEventListener("click", renderWalkerList);
  document.getElementById("searchName").addEventListener("input", renderWalkerList);
  document.getElementById("searchUsername").addEventListener("input", renderWalkerList);
  document.getElementById("searchStatus").addEventListener("change", renderWalkerList);
  
  // Inicializar
  renderWalkerList();
  