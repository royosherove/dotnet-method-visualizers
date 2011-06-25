using System;
using System.Diagnostics;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Reflection;
using System.Reflection.Emit;

[assembly: DebuggerVisualizer(
    typeof(DynamicMethodVisualizer.MethodBodyVisualizer),
    typeof(DynamicMethodVisualizer.MethodBodyObjectSource),
    Target = typeof(DynamicMethod),
    Description = "Method IL Visualizer")
]

[assembly: DebuggerVisualizer(
    typeof(DynamicMethodVisualizer.MethodBodyVisualizer),
    typeof(DynamicMethodVisualizer.MethodBodyObjectSource),
    Target = typeof(MethodInfo),
    Description = "Method IL Visualizer")
]

[assembly: DebuggerVisualizer(
    typeof(DynamicMethodVisualizer.MethodBodyVisualizer),
    typeof(DynamicMethodVisualizer.MethodBodyObjectSource),
    Target = typeof(MethodBase),
    Description = "Method IL Visualizer")
]

[assembly: DebuggerVisualizer(
    typeof(DynamicMethodVisualizer.MethodBodyVisualizer),
    typeof(DynamicMethodVisualizer.MethodBodyObjectSource),
    Target = typeof(Delegate),
    Description = "Method IL Visualizer")
]

namespace DynamicMethodVisualizer
{
    public class MethodBodyVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            using (MethodBodyViewer viewer = new MethodBodyViewer())
            {
                viewer.SetObjectProvider(objectProvider);
                viewer.ShowDialog();
            }
        }
    }
}
