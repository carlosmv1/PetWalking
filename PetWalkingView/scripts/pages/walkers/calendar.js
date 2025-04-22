const days = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];
const events = [
  { day: 0, hour: 6, label: "Meeting w/ Ely", color: "bg-yellow-200" },
  { day: 0, hour: 7, label: "Team Catch-up", color: "bg-blue-200" },
  { day: 2, hour: 7, label: "Product Meeting", color: "bg-orange-200" },
  { day: 4, hour: 7, label: "Weekly Review", color: "bg-blue-200" },
];

const calendar = document.getElementById("calendarDays");

days.forEach((day, dayIndex) => {
  const col = document.createElement("div");
  col.className = "border-l border-gray-200 relative";

  // Encabezado del día
  col.innerHTML = `
    <div class="h-12 flex items-center justify-center font-semibold bg-gray-50 border-b border-gray-200">${day}</div>
    ${[6, 7, 8, 9, 10, 11, 12].map(() => `<div class="h-12 border-t border-gray-100"></div>`).join("")}
  `;

  // Insertar eventos simulados
  events
    .filter(event => event.day === dayIndex)
    .forEach(event => {
      const box = document.createElement("div");
      box.className = `absolute top-[${(event.hour - 6) * 3}rem] left-2 right-2 ${event.color} p-2 rounded shadow-sm text-xs`;
      box.textContent = event.label;
      col.appendChild(box);
    });

  calendar.appendChild(col);
});
