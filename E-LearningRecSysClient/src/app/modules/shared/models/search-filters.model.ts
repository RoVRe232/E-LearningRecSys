import { ThemePalette } from '@angular/material/core';

export interface SearchFiltersModel {
  name: string;
  completed: boolean;
  color: ThemePalette;
  subFilters?: SearchFiltersModel[];
}
