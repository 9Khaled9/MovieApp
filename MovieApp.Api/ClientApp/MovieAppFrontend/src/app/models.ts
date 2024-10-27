export interface MovieDto {
    id: number;
    title: string;
    overview: string;
    backdropPath: string;
    releaseDate: string;
    voteAverage: number;
}

export interface MovieDetailDto {
    id: number;
    title: string;
    overview: string;
    backdropPath: string;
    releaseDate: string;
    voteAverage: number;
    rating: number | null;
    originalLanguage: string | null;
    popularity: number | null;
    voteCount: number | null;
    runtime: number | null;
    budget: number | null;
    revenue: number | null;
    genres: GenreDto[];
}

export interface GenreDto {
    id: number | null;
    name: string | null;
}


