// Copyright (c) Meta Platforms, Inc. and affiliates.

namespace Oculus.Interaction.MoveFast
{
    public class StringPropertyBehaviour : PropertyBehaviour<string>
    {
        public string startValue = "";

        private void Awake()
        {
            if (string.IsNullOrEmpty(Value))
            {
                Value = startValue;
            }
        }
    }
}
