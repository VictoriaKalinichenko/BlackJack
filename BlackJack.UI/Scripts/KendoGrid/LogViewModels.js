$(document).ready(function () {
    dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Log/GetLogs",
                dataType: "json",
                type: "GET"
            },
            error: function (e) {
                alert(`Status: ${e.status}; Error message: ${e.errorThrown}`);
            }
        },
        pageSize: 20,
        schema: {
            model: {
                id: "Id",
                fields: {
                    Id: {},
                    GameId: {},
                    DateTime: {type: "date"},
                    Message: {}
                }
            }
        }
    });

    $("#grid").kendoGrid({
        dataSource: dataSource,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        sortable: true,
        filterable: true,
        groupable: true,
        height: 550,
        columns: [
            { field: "Id", title: "Id", width: 80 },
            { field: "GameId", title: "GameId", width: 120 },
            {
                field: "DateTime",
                title: "Data/Time",
                width: 200,
                template: "#= kendo.toString(kendo.parseDate(DateTime), 'MM/dd/yyyy hh:mm:ss.fff tt') #",
                filterable: {
                    ui: "datetimepicker",
                    operators: {
                        date: {
                            gt: "After",
                            lt: "Before"
                        }
                    }
                }
            },
            { field: "Message", title: "Message" }
        ]
    });
});