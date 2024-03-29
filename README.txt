Kısım 1
Fonksiyonel Gereksinimler
Departman ve doktor yönetimi (CRUD) => Bu gereksinim Departman.API projesi altındaki DepartmentsController ve DoctorsController içerisinde yönetilmektedir.

Online randevu yönetimi => Bu gereksinim Appointment.API projesi ile yönetilmektedir.

Hasta geçmişi görüntüleme  => Bu gereksinim Appointment.API projesi altındaki PatientHistoryController içerisinde yönetilmektedir. Hastaya ait tüm randevuları ve hastaya ait belirtilen filtrelerdeki randevuları getiren 2 adet endpoint mevcuttur.

Doktora soru sorma ve doktor tarafından cevaplanabilme => Bu gereksinim AnswerQuestion.API projesi altındaki AnswersController ve QuestionsController içerisinde yönetilmektedir.

Hastalara bildirim gönderme => Bu gereksinim Notification.API projesi ile yönetilmektedir.

*Ayrıca hasta create işlemi ve hasta bilgisine ihtiyaç duyan API'lara bu bilgiyi MessageBroker aracılığıyla sağlamak amaçlı olarak Patient.API projesi de eklenmiştir.


Kısım 2
Servisleri Docker ile dockerize ediniz ve Kubernetes ile deployment’i tamamlayınız.
=> Dokerize etme işlemi için API projelerinde Dockerfile'lar oluşturuldu.
=> Kubernetes ortamına deployment işlemi için Deployment.yml dosyaları ile image'ların Kubernetes ortamında çalışması sağlandı. Service.yml dosyaları ile uygulamalar expose edildi.


-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-

MessageBroker Mekanizması
MessageBroker.Base projesi => Temel tanımlamaları ve tek bir kütüphaneye bağımlı kalınmaması için kullanılacak olan base mekanizmanın kodlandığı class library.
MessageBroker.Provider projesi => Brokerı kullanacak olan uygulamaların hangi tipteki brokerı kullanacağını belirterek bir IMessageBroker interface'i inject etmelerini sağlayan
									mekanizmanın kodlandığı class library.
MessageBroker.RabbitMQ => MessageBrokerBase'den türeyen ve RabbitMQ özelindeki kodların yazıldığı (publish, queue declare, queue bind, subscribe vs) ve retry mekanizmasının kodlandığı
							class library.
							
							
							
-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-o-

Örnek Akış
Hasta Patient.API aracılığıyla yaratılır.
MessageBroker aracılığıyla Notification.API hasta bilgisini alır.
Appointment.API aracılığıyla randevu verilir.
Randevu create işlemi sonrası randevu yaratıldı bilgisi (AppointmentCreatedEvent) publish edilir.
Notification.API içerisindeki AppointmentCreatedEventHandler AppointmentCreatedEvent'e subscribe olarak (Subscribe olma işlemi Notification.API Program.cs içerisindeki AddEventSubscriptions metodunda yapılır. ) Notification bilgisini kaydeder.