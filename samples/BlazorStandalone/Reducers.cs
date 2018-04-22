﻿using System;
using System.Collections.Generic;
using BlazorRedux;

namespace BlazorStandalone
{
    public static class Reducers
    {
        public static MyState RootReducer(MyState state, IAction action)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            return new MyState
            {
                Location = state.Location,
                Count = CountReducer(state.Count, action),
                Forecasts = ForecastsReducer(state.Forecasts, action)
            };
        }

        private static int CountReducer(int count, IAction action)
        {
            switch (action)
            {
                case IncrementByOneAction _:
                    return count + 1;
                case IncrementByValueAction a:
                    return count + a.Value;
                default:
                    return count;
            }
        }

        private static IEnumerable<WeatherForecast> ForecastsReducer(IEnumerable<WeatherForecast> forecasts,
            IAction action)
        {
            switch (action)
            {
                case ClearWeatherAction _:
                    return null;
                case ReceiveWeatherAction a:
                    return a.Forecasts;
                default:
                    return forecasts;
            }
        }

        public static MyState LocationReducer(MyState state, LocationAction action)
        {
            var newState = RootReducer(state, null);

            switch (action)
            {
                case NewLocationAction a:
                    newState.Location = a.Location;
                    break;
            }

            return newState;
        }
    }
}