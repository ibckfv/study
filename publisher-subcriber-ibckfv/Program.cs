using publisher_subcriber_ibckfv;

ActionsInfoHub actionsInfoHub = new();
MobileMaps mobileMaps = new();
EmergencyServices emergencyServices = new();

actionsInfoHub.OnActionsUpdates += mobileMaps.Update;
actionsInfoHub.OnActionsUpdates += emergencyServices.Update;
actionsInfoHub.UpdateInformation(new TransportAction(0, "accident"));