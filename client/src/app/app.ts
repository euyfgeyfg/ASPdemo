import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit{
  private http = inject(HttpClient);// 已经完成HttpClient注入
  
  protected readonly title = "Demo"
  protected members = signal<any>([])


  async ngOnInit(){
    this.members.set(await this.getMembers())
  }

  async getMembers(){
    try{
      return lastValueFrom(this.http.get("http://localhost:5001/api/members"));
    }catch(error){
      console.log(error);
      throw error;
    }
  }
}
