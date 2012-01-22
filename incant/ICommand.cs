using System;

namespace incant
{
	public interface ICommand<InputType, OutputType>
	{
		OutputType execute(InputType input);
	}
}

