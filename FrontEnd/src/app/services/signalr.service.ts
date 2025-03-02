import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  private readonly hubConnection: HubConnection;

  public date: Date | undefined;


  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:7101/signalR')
      .build();
  }

  getConnection(): HubConnection {
    return this.hubConnection;
  }

  async connect(): Promise<void> {
    try {
      await this.hubConnection.start();

      if(this.hubConnection.connectionId)
      this.hubConnection.on('SendServerTimeAsync', (result) => {
        this.date = result as Date;
      });
    } catch (error) {
      console.error(error);
    }
  }

  //  async serverTime():Promise<void>{

  //   try{
  //     await this.hubConnection.invoke("ServerTime");
  //     console.log('serverTime');

  //   }catch(error){
  //     console.error(error)
  //   }
  //  }

  //  public connectSignalR():void{
  //   this.connect().then(()=>{
  //     this.serverTime();

  //     setInterval(() => {
  //       this.serverTime();
  //     }, 60000);
  //     this.hubConnection.on("ServerTime",(date:Date)=>{
  //       this.date=date;
  //       console.log(date.getSeconds());

  //     })
  //   })
  //  }
}
