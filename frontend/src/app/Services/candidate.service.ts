import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface Candidate {
    id: string
    name: string
    stage: string
    phone: string
    email: string
  }

@Injectable({ providedIn: 'root' })
export class CandidateService {

    private apiUrl = 'https://localhost:7258/api/candidates';
  
    constructor(private httpClient: HttpClient) { }    
    
    createcandidate = (createdData: Candidate) => this.httpClient.post<Candidate>(`${this.apiUrl}`, createdData);
  
    getcandidates = () => this.httpClient.get<Candidate[]>(`${this.apiUrl}`);
  
    updatecandidate = (updateData: Candidate, candidateId: string) => this.httpClient.put<Candidate>(`${this.apiUrl}/${candidateId}`, updateData);
  
    deletecandidate = (candidateId: string) => this.httpClient.delete(`${this.apiUrl}/${candidateId}`);
  
  }