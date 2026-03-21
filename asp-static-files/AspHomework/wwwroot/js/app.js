document.addEventListener('DOMContentLoaded', () => {
    const inventoryView = document.getElementById('view-inventory-view');
    const addItemView = document.getElementById('add-item-view');

    const navInventoryBtn = document.getElementById('nav-inventory');
    const navAddBtn = document.getElementById('nav-add');

    const categoryFilterInput = document.getElementById('categoryFilter');
    const minCountFilterInput = document.getElementById('minCountFilter');
    const applyFiltersBtn = document.getElementById('applyFilters');

    const inventoryGrid = document.getElementById('inventoryGrid');
    const statsContainer = document.getElementById('stats');
    const loadingContainer = document.getElementById('loading');
    const emptyContainer = document.getElementById('emptyInventory');

    const addNameInput = document.getElementById('itemName');
    const addCountInput = document.getElementById('itemCount');
    const addCategoryInput = document.getElementById('itemCategory');
    const addSubmitBtn = document.getElementById('submitAddItem');

    function showView(view) {
        if (view === 'inventory') {
            inventoryView.style.display = '';
            addItemView.style.display = 'none';
            navInventoryBtn.classList.add('active');
            navAddBtn.classList.remove('active');
        } else {
            inventoryView.style.display = 'none';
            addItemView.style.display = '';
            navInventoryBtn.classList.remove('active');
            navAddBtn.classList.add('active');
        }
    }

    function showMessage(text, type = 'success') {
        const message = document.createElement('div');
        message.className = `message ${type}`;
        message.textContent = text;
        document.body.appendChild(message);

        setTimeout(() => {
            message.remove();
        }, 3000);
    }

    function setLoading(isLoading) {
        if (!loadingContainer) return;
        loadingContainer.style.display = isLoading ? 'block' : 'none';
    }

    function renderStats(data) {
        if (!statsContainer) return;

        const stats = data.stats || {};
        statsContainer.innerHTML = `
            <div class="stat-item">
                <div class="stat-value">${data.totalItems ?? 0}</div>
                <div class="stat-label">Всего предметов</div>
            </div>
            <div class="stat-item">
                <div class="stat-value">${data.filteredCount ?? 0}</div>
                <div class="stat-label">Отфильтровано</div>
            </div>
            <div class="stat-item">
                <div class="stat-value">${stats.uniqueItems ?? 0}</div>
                <div class="stat-label">Уникальные предметы</div>
            </div>
            <div class="stat-item">
                <div class="stat-value">${stats.totalCount ?? 0}</div>
                <div class="stat-label">Всего в стакax</div>
            </div>
            <div class="stat-item">
                <div class="stat-value">${stats.mostCommonCategory ?? 'Нет'}</div>
                <div class="stat-label">Частая категория</div>
            </div>
        `;
    }

    function renderInventory(data) {
        if (!inventoryGrid || !emptyContainer) return;

        const items = data.items || [];

        if (items.length === 0) {
            inventoryGrid.innerHTML = '';
            emptyContainer.style.display = 'block';
            return;
        }

        emptyContainer.style.display = 'none';

        inventoryGrid.innerHTML = items.map(item => {
            const category = item.category || 'Нет';
            return `
                <div class="item-card" data-id="${item.id}">
                    <button class="remove-btn" data-id="${item.id}" title="Удалить предмет">×</button>
                    <h3>${item.name}</h3>
                    <p class="item-info">
                        ID: <span>${item.id}</span>
                    </p>
                    <p class="item-info">
                        Количество: <span>${item.count}</span>
                    </p>
                    <p class="item-info">
                        Категория: <span>${category}</span>
                    </p>
                </div>
            `;
        }).join('');
    }

    async function loadInventory() {
        try {
            setLoading(true);

            const category = categoryFilterInput?.value || '';
            const minCount = minCountFilterInput?.value || '';

            const params = new URLSearchParams();
            if (category) params.append('category', category);
            if (minCount) params.append('minCount', minCount);

            const response = await fetch(`/inventory/view?${params.toString()}`, {
                headers: { 'Accept': 'application/json' }
            });

            if (!response.ok) {
                throw new Error('Не удалось загрузить инвентарь');
            }

            const data = await response.json();
            renderStats(data);
            renderInventory(data);
        } catch (e) {
            console.error(e);
            showMessage('Ошибка при загрузке инвентаря', 'error');
        } finally {
            setLoading(false);
        }
    }

    async function addItem() {
        try {
            const name = addNameInput.value.trim();
            const count = parseInt(addCountInput.value, 10);
            const category = addCategoryInput.value.trim();

            if (!name || !Number.isFinite(count) || count <= 0) {
                showMessage('Введите корректные данные', 'error');
                return;
            }

            const item = { name, count, category };

            const response = await fetch('/inventory/add', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(item)
            });

            const result = await response.json();

            if (!response.ok || !result.success) {
                throw new Error(result.message || 'Ошибка при добавлении предмета');
            }

            showMessage(result.message || 'Предмет добавлен', 'success');

            addNameInput.value = '';
            addCountInput.value = '1';
            addCategoryInput.value = '';

            showView('inventory');
            await loadInventory();
        } catch (e) {
            console.error(e);
            showMessage(e.message || 'Ошибка при добавлении предмета', 'error');
        }
    }

    async function removeItem(id) {
        if (!id) return;

        if (!confirm('Удалить предмет?')) {
            return;
        }

        try {
            const response = await fetch(`/inventory/remove?id=${id}`, {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json'
                }
            });

            const result = await response.json().catch(() => ({}));

            if (!response.ok || result.success === false) {
                throw new Error(result.message || 'Ошибка при удалении предмета (эндпоинт может быть не реализован)');
            }

            showMessage(result.message || 'Предмет удалён', 'success');
            await loadInventory();
        } catch (e) {
            console.error(e);
            showMessage(e.message || 'Ошибка при удалении предмета', 'error');
        }
    }

    // Навигация
    navInventoryBtn.addEventListener('click', () => {
        showView('inventory');
        loadInventory();
    });

    navAddBtn.addEventListener('click', () => {
        showView('add');
    });

    // Фильтры
    applyFiltersBtn.addEventListener('click', () => {
        loadInventory();
    });

    inventoryGrid.addEventListener('click', (e) => {
        const target = e.target;
        if (target.classList.contains('remove-btn')) {
            const id = parseInt(target.getAttribute('data-id'), 10);
            if (Number.isFinite(id)) {
                removeItem(id);
            }
        }
    });

    // Добавление предмета
    addSubmitBtn.addEventListener('click', (e) => {
        e.preventDefault();
        addItem();
    });

    // Начальное состояние
    showView('inventory');
    loadInventory();
});

