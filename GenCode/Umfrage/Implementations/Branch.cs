﻿using System;
using System.Collections.Generic;

using Umfrage.Abstractions;
namespace Umfrage.Implementations
{

	public class Branch : IBranch
    {

        public string Name { get; }

        public Branch( string name )
        {
            Name = name;
            _questions = new List<IQuestion>( );
        }

        private List<IQuestion> _questions;
        public IEnumerable<IQuestion> Questions => _questions;

        public IBranch Add( IQuestion question, int? position = null )
        {
            if ( position < 0 ) {
                throw new InvalidOperationException( $"The {nameof( position )} is invalid" );
            }

            if ( position.HasValue )
            {
                _questions.Insert( position.Value, question );
            } else
            {
                _questions.Add( question );
            }

            return this;
        }
    }
}
