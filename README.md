# Sosyal Medya Üzerinde İnsan Arama
<h4>Nedir?</h4>
Aradığınız kişiyi sadece ad ve soyad bilgisi girerek sosyal medya api'leri ile o kişiye ait sosyal medya hesaplarını tek bir site üzerinden bulmayı sağlar. Bunun için sosyal medya API servislerini kullanarak o isimdeki kişileri bulur ve daha sonra bu kişilerin resimlerini karşılaştırarak benzerlik skoru çıkarır. Eğer benzerlik varsa şuan için facebook ve twitter hesaplarını sisteme kaydeder.

<h4>Çalışma mantığı?</h4>
<ul>
<li>Aranan ismi Facebook search apisi ile arar</li>
<li>Daha sonra aynı ismi Twitter search apisi ile arar</li>
<li><a target="_blank" href="https://azure.microsoft.com/en-us/services/cognitive-services/face/">Azure Face API</a> kullanarak her Facebook kullanıcısı için Twitter kullanıcısının resmi ile karşılaştırıp bir skor elde edilir</li>
<li>Elde edilen skor benzerlik için yeterli ise veritabanına ekler</li>
<li>Aranan adı veritabanıdan arayarak sitede gösterir</li>
</ul>

<h4>Kullanımı</h4>
<ul>
<li>Projeyi bilgisayarınıza klonladıktan sonra Solution'a sağ tıklayıp Restore Nuget Packages tıklayakrak paketleri yükleyin</li>
<li><a target="_blank" href="https://developers.facebook.com/">Facebook Developer</a> üzerinden facebook api hesabı açın daha sonra gerekli bilgileri projedeki Services klasörü altındaki FacebookService sınıfının fields kısmındaki bilgileri doldururnuz.</li>
<li>Aynı işlemi <a target="_blank" href="https://dev.twitter.com/">Twitter Developer</a> için de yapın. TwitterService içindeki fields kısmında bulunan bilgileri doldurun</li>
<li>Son olarak yüzleri karşılaştırmak için gerekli olan <a target="_blank" href="https://azure.microsoft.com/tr-tr/try/cognitive-services/">Face API</a> için SubscriptionKey oluşturmanız ve projedeki FaceService sınıfının SubscriptionKey propertiesine set edin</li>
<li>Derleyip çalıştırın</li>
</ul>

<h4>Olası Durumlar</h4>
<p>Aranan kullanıcı sosyal medya hesapların ayarlarında 'dışındaki arama motorlarının, profiline bağlantı vermesine izin ver' özelliğini kapatırmış ise API o kullanıcıyı göstermediği için eşleşme olmayacaktır. Diğer bir durum ise Face API tarafından profil resimleri tanımlamama durumunda eşleşme olmama olasığı vardır. Özellikle ünlülerde fake hesapları orjinal hesapla eşleştiriyor böyle bir durumda var.</p>

<h4>Yapılacaklar</h4>
<ul>
<li>Instagram arama desteği</li>
<li>LinkedIn arama desteği</li>
<li>Foursquare arama desteği</li>
<li>Diğer sosyal medya arama apileri</li>
</ul>
