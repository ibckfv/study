using ReflectionTest;

var adminsHandler = new AdminAuditHandler();
var securityHandler = new SecurityNotificationHandler();
var logger = new LogAllHandler();

var bus = new EventBus();

bus.Publish(new UserEnteredEvent { Username = "Ivanov", Role = Role.Admin});
bus.Publish(new UserEnteredEvent { Username = "Petrov", Role = Role.Visitor});
bus.Publish(new AccessDeniedEvent { Username = "Sidorov", Reason = "Invalid pass" });