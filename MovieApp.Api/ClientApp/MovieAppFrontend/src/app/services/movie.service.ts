import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MovieDto, MovieDetailDto } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  private apiUrl = `${environment.apiBaseUrl}/movies`;

  constructor(private http: HttpClient) {}

  getPopularMovies(): Observable<MovieDto[]> {
    return this.http.get<MovieDto[]>(`${this.apiUrl}/popular`);
  }

  getMovieDetails(movieId: number): Observable<MovieDetailDto> {
    return this.http.get<MovieDetailDto>(`${this.apiUrl}/${movieId}`);
  }

  addToOfflineList(movieId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/offline/${movieId}`, {});
  }
}
