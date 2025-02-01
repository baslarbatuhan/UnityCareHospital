<script>
    $(function () {
        var reservedAppointments = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(ViewBag.ReservedAppointments));

    $("#datetimepicker").datetimepicker({
        dateFormat: "dd-mm-yy",
    timeFormat: "HH:mm",
    stepMinute: 30, // 30 dakikalık aralıklar
    minDate: 0, // Bugünden itibaren tarih seçilebilir
    beforeShowDay: function (date) {
                // Tarihi engelleme
                var formattedDate = $.datepicker.formatDate("yy-mm-dd", date);
    return reservedAppointments.some(function (appt) {
                    return appt.Date === formattedDate;
                })
    ? [false, "", "Bu tarih dolu"]
    : [true, "", ""];
            },
    onSelect: function (dateText) {
                // Saati engelleme
                var selectedDate = $.datepicker.parseDate("dd-mm-yy", dateText);
    var reservedTimes = reservedAppointments
    .filter(function (appt) {
                        return appt.Date === $.datepicker.formatDate("yy-mm-dd", selectedDate);
                    })
    .map(function (appt) {
                        return [appt.Time, appt.Time];
                    });

    $("#timepicker").timepicker({
        step: 30,
    disableTimeRanges: reservedTimes,
                });
            },
        });
    });

    function showPopup(message) {
        $("#popupMessage").text(message); // Mesajı güncelle
    $("#successPopup").fadeIn(); // Pop-up'ı göster
        }

    // Pop-up'ı kapatan fonksiyon
    function closePopup() {
        $("#successPopup").fadeOut(); // Pop-up'ı gizle
        }

    // TempData üzerinden mesaj kontrolü
    $(document).ready(function () {
            var successMessage = "@TempData["SuccessfullAppointment"]";

    if (successMessage) {
        showPopup(successMessage);
            }
        });

    let currentWeekOffset = 0;

    function updateCalendar() {
            const calendar = document.getElementById('calendar');
    const weekInfo = document.getElementById('week-info');
    const days = ['Pzt', 'Sal', 'Çar', 'Per', 'Cum'];
    const times = ['09:00', '10:00', '11:00', '12:00', '13:00', '14:00', '15:00', '16:00', '17:00'];

    // Clear existing calendar
    calendar.innerHTML = '';

    // Add headers
    calendar.innerHTML += '<div class="header"></div>';
            days.forEach(day => {
        calendar.innerHTML += `<div class="header">${day}</div>`;
            });

            // Add time slots
            times.forEach(time => {
        calendar.innerHTML += `<div class="time">${time}</div>`;
    for (let i = 0; i < days.length; i++) {
        calendar.innerHTML += '<div></div>';
                }
            });

    // Calculate current week
    const currentDate = new Date();
    currentDate.setDate(currentDate.getDate() + currentWeekOffset * 7);
    const startOfWeek = new Date(currentDate.setDate(currentDate.getDate() - currentDate.getDay() + 1));
    const endOfWeek = new Date(currentDate.setDate(currentDate.getDate() - currentDate.getDay() + 5));

    // Update week info
    weekInfo.innerHTML = `Hafta: ${startOfWeek.toLocaleDateString()} - ${endOfWeek.toLocaleDateString()} (${startOfWeek.getFullYear()})`;
        }

    function previousWeek() {
        currentWeekOffset--;
    updateCalendar();
        }

    function nextWeek() {
        currentWeekOffset++;
    updateCalendar();
        }

    // Initialize calendar
    updateCalendar();

</script>
