// Bu fonksiyon localStorage'daki tüm verileri alır
function getAllLocalStorageData() {
    var localStorageData = {};

    for (var i = 0; i < localStorage.length; i++) {
        var key = localStorage.key(i);
        var value = localStorage.getItem(key);
        localStorageData[key] = value;
    }

    return localStorageData;
}

// Checkout butonuna tıklandığında yapılacak işlemler
document.getElementById('checkoutButton').addEventListener('click', function (event) {
    event.preventDefault(); // Formun varsayılan gönderme işlemini engeller

    var allData = getAllLocalStorageData();
    var addressId = document.querySelector('select[name="Id"]').value; // Seçili adres ID'sini alır

    // POST verisini hazırlar
    var postData = {
        addressId: addressId,
        cartData: allData
    };

    // Sunucuya POST isteği gönderir
    fetch('/Cart/Checkout', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(postData)
    })
        .then(response => response.json())
        .then(data => {
            if (data.redirectUrl) {
                window.location.href = data.redirectUrl;
                localStorage.clear();
            } else {
                console.error('Yönlendirme URL alınamadı');
            }
        })
        .catch(error => {
            console.error('Hata oluştu:', error); // Hata durumunda konsola hata mesajı yazar
        });
});