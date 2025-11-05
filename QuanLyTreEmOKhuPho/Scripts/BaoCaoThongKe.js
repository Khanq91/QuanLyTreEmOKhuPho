// Scripts/BaoCaoThongKe.js
$(document).ready(function () {
    // Cấu hình chung cho tất cả biểu đồ
    const commonOptions = {
        responsive: true,
        maintainAspectRatio: true,
        plugins: {
            legend: {
                position: 'bottom',
                labels: {
                    padding: 15,
                    font: {
                        size: 12
                    }
                }
            }
        }
    };

    // 1. Biểu đồ Sự kiện theo thời gian (Line Chart)
    const eventsCtx = document.getElementById('eventsChart');
    if (eventsCtx) {
        new Chart(eventsCtx.getContext('2d'), {
            type: 'line',
            data: {
                labels: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6'],
                datasets: [{
                    label: 'Số sự kiện',
                    data: [12, 15, 18, 22, 25, 28],
                    borderColor: 'rgb(99, 102, 241)',
                    backgroundColor: 'rgba(99, 102, 241, 0.1)',
                    tension: 0.4,
                    fill: true,
                    borderWidth: 3
                }]
            },
            options: {
                ...commonOptions,
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            stepSize: 5
                        }
                    }
                }
            }
        });
    }

    // 2. Biểu đồ Trẻ em được hỗ trợ (Bar Chart)
    const childrenCtx = document.getElementById('childrenChart');
    if (childrenCtx) {
        new Chart(childrenCtx.getContext('2d'), {
            type: 'bar',
            data: {
                labels: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6'],
                datasets: [{
                    label: 'Số trẻ',
                    data: [180, 220, 250, 280, 310, 340],
                    backgroundColor: 'rgba(34, 197, 94, 0.8)',
                    borderColor: 'rgb(34, 197, 94)',
                    borderWidth: 1
                }]
            },
            options: {
                ...commonOptions,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    }

    // 3. Biểu đồ Nguồn ủng hộ (Doughnut Chart)
    const donationsCtx = document.getElementById('donationsChart');
    if (donationsCtx) {
        new Chart(donationsCtx.getContext('2d'), {
            type: 'doughnut',
            data: {
                labels: ['Tiền mặt', 'Chuyển khoản', 'Hiện vật', 'Khác'],
                datasets: [{
                    data: [450, 395, 155, 45],
                    backgroundColor: [
                        'rgba(251, 146, 60, 0.8)',
                        'rgba(59, 130, 246, 0.8)',
                        'rgba(168, 85, 247, 0.8)',
                        'rgba(236, 72, 153, 0.8)'
                    ],
                    borderWidth: 2,
                    borderColor: '#fff'
                }]
            },
            options: commonOptions
        });
    }

    // 4. Biểu đồ Tình nguyện viên (Line Chart)
    const volunteersCtx = document.getElementById('volunteersChart');
    if (volunteersCtx) {
        new Chart(volunteersCtx.getContext('2d'), {
            type: 'line',
            data: {
                labels: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6'],
                datasets: [{
                    label: 'Tình nguyện viên',
                    data: [120, 135, 145, 158, 172, 186],
                    borderColor: 'rgb(239, 68, 68)',
                    backgroundColor: 'rgba(239, 68, 68, 0.1)',
                    tension: 0.4,
                    fill: true,
                    borderWidth: 3
                }]
            },
            options: {
                ...commonOptions,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    }

    // 5. Biểu đồ So sánh hiệu suất theo tháng (Multi-bar Chart)
    const comparisonCtx = document.getElementById('comparisonChart');
    if (comparisonCtx) {
        new Chart(comparisonCtx.getContext('2d'), {
            type: 'bar',
            data: {
                labels: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6'],
                datasets: [
                    {
                        label: 'Sự kiện',
                        data: [12, 15, 18, 22, 25, 28],
                        backgroundColor: 'rgba(99, 102, 241, 0.8)',
                        borderColor: 'rgb(99, 102, 241)',
                        borderWidth: 1
                    },
                    {
                        label: 'Trẻ em (x10)',
                        data: [18, 22, 25, 28, 31, 34],
                        backgroundColor: 'rgba(34, 197, 94, 0.8)',
                        borderColor: 'rgb(34, 197, 94)',
                        borderWidth: 1
                    },
                    {
                        label: 'Tình nguyện viên',
                        data: [120, 135, 145, 158, 172, 186],
                        backgroundColor: 'rgba(251, 146, 60, 0.8)',
                        borderColor: 'rgb(251, 146, 60)',
                        borderWidth: 1
                    }
                ]
            },
            options: {
                ...commonOptions,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    }

    // 6. Biểu đồ Phân loại sự kiện (Pie Chart)
    const eventTypeCtx = document.getElementById('eventTypeChart');
    if (eventTypeCtx) {
        new Chart(eventTypeCtx.getContext('2d'), {
            type: 'pie',
            data: {
                labels: ['Giáo dục', 'Y tế', 'Giải trí', 'Từ thiện', 'Khác'],
                datasets: [{
                    data: [30, 25, 20, 15, 10],
                    backgroundColor: [
                        'rgba(99, 102, 241, 0.8)',
                        'rgba(34, 197, 94, 0.8)',
                        'rgba(251, 146, 60, 0.8)',
                        'rgba(168, 85, 247, 0.8)',
                        'rgba(236, 72, 153, 0.8)'
                    ],
                    borderWidth: 2,
                    borderColor: '#fff'
                }]
            },
            options: commonOptions
        });
    }

    // 7. Biểu đồ Tỷ lệ hoàn thành mục tiêu (Radar Chart)
    const goalCtx = document.getElementById('goalChart');
    if (goalCtx) {
        new Chart(goalCtx.getContext('2d'), {
            type: 'radar',
            data: {
                labels: ['Sự kiện', 'Trẻ em', 'Ủng hộ', 'Tình nguyện viên', 'Giờ tình nguyện'],
                datasets: [{
                    label: 'Hoàn thành (%)',
                    data: [85, 92, 78, 88, 95],
                    backgroundColor: 'rgba(99, 102, 241, 0.2)',
                    borderColor: 'rgb(99, 102, 241)',
                    pointBackgroundColor: 'rgb(99, 102, 241)',
                    pointBorderColor: '#fff',
                    pointHoverBackgroundColor: '#fff',
                    pointHoverBorderColor: 'rgb(99, 102, 241)',
                    borderWidth: 2
                }]
            },
            options: {
                ...commonOptions,
                scales: {
                    r: {
                        beginAtZero: true,
                        max: 100,
                        ticks: {
                            stepSize: 20
                        }
                    }
                }
            }
        });
    }

    // Điền dữ liệu cho bảng tuần
    const weeklyData = [
        { week: 'Tuần 1', events: 12, children: 310, donation: '215M', items: 580, volunteers: 45, hours: 850 },
        { week: 'Tuần 2', events: 10, children: 285, donation: '198M', items: 520, volunteers: 42, hours: 780 },
        { week: 'Tuần 3', events: 14, children: 340, donation: '235M', items: 620, volunteers: 52, hours: 920 },
        { week: 'Tuần 4', events: 12, children: 310, donation: '197M', items: 620, volunteers: 47, hours: 870 }
    ];

    const weeklyTableBody = $('#weeklyTable');
    weeklyData.forEach(row => {
        const tr = `
            <tr>
                <td>${row.week}</td>
                <td>${row.events}</td>
                <td>${row.children}</td>
                <td>${row.donation}</td>
                <td>${row.items}</td>
                <td>${row.volunteers}</td>
                <td>${row.hours}</td>
            </tr>
        `;
        weeklyTableBody.append(tr);
    });

    // Điền dữ liệu cho bảng top tình nguyện viên
    const topVolunteers = [
        { rank: 1, name: 'Nguyễn Văn An', events: 25, hours: 520, rating: 5 },
        { rank: 2, name: 'Trần Thị Bích', events: 23, hours: 480, rating: 5 },
        { rank: 3, name: 'Lê Văn Cường', events: 22, hours: 450, rating: 5 },
        { rank: 4, name: 'Phạm Thị Dung', events: 20, hours: 420, rating: 4 },
        { rank: 5, name: 'Hoàng Văn Em', events: 19, hours: 400, rating: 4 },
        { rank: 6, name: 'Vũ Thị Phương', events: 18, hours: 380, rating: 5 },
        { rank: 7, name: 'Đặng Văn Giang', events: 17, hours: 360, rating: 4 },
        { rank: 8, name: 'Ngô Thị Hoa', events: 16, hours: 340, rating: 4 },
        { rank: 9, name: 'Bùi Văn Ích', events: 15, hours: 320, rating: 4 },
        { rank: 10, name: 'Phan Thị Kim', events: 14, hours: 300, rating: 5 }
    ];

    const topVolunteersTableBody = $('#topVolunteersTable');
    topVolunteers.forEach(row => {
        const stars = '⭐'.repeat(row.rating);
        const tr = `
            <tr>
                <td>${row.rank}</td>
                <td>${row.name}</td>
                <td>${row.events}</td>
                <td>${row.hours}</td>
                <td>${stars}</td>
            </tr>
        `;
        topVolunteersTableBody.append(tr);
    });
});