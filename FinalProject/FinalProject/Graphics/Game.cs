using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace INFR2100U.Graphics;

public class Game : GameWindow
{
    private Shader shader;
    private Texture tex0;
    private Texture tex1;
    
    private int vertexBufferObject;
    private int vertexArrayObject;
    private int elementBufferObject;
    
    // Position + UVs
    private readonly float[] verts =
    {
        -1, 1, 0,   0, 1, // Top-left
        -1, -1, 0,  0, 0, // Bottom-left
        1, -1, 0,   1, 0, // Bottom-right
        1, 1, 0,    1, 1  // Top-right
    };
    
    private uint[] indices =
    {
        0, 1, 2,
        0, 2, 3
    };
    
    public Game(int width, int height, string title) : base(GameWindowSettings.Default,
        new NativeWindowSettings() { ClientSize = new Vector2i(width, height), Title = title })
    {
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, e.Width, e.Height);
        base.OnResize(e);
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.302f, 0.302f, 0.302f, 1f);
        
        shader = new Shader("default.vert", "default.frag");
        
        // === TEXTURES === //
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        
        tex0 = new Texture("Container.jpg");
        tex0.Use(TextureUnit.Texture0);
        tex1 = new Texture("Bender.png");
        tex1.Use(TextureUnit.Texture1);

        // === INIT VBO === //
        vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * verts.Length, verts, BufferUsageHint.StaticDraw);
        
        // === INIT VAO === //
        vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(vertexArrayObject);
        
        // === INIT EBO === //
        elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * indices.Length, indices, BufferUsageHint.StaticDraw);

        // === SET ATTRIBUTES === //
        int id = shader.GetAttribLocation("aPos");
        GL.VertexAttribPointer(id, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        GL.EnableVertexAttribArray(id);

        id = shader.GetAttribLocation("UVs");
        GL.VertexAttribPointer(id, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(id);

        shader.Use();
        
        // === SET UNIFORMS === //
        id = shader.GetUniformLocation("color");
        GL.Uniform4(id, Color4.Aqua);
        
        id = shader.GetUniformLocation("tex0");
        GL.Uniform1(id, 0);
        
        id = shader.GetUniformLocation("tex1");
        GL.Uniform1(id, 1);
    }

    protected override void OnUnload()
    {
        // === CLEAN UP === //
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.UseProgram(0);
        
        GL.DeleteBuffer(vertexBufferObject);
        GL.DeleteBuffer(vertexArrayObject);
        GL.DeleteBuffer(elementBufferObject);
        
        shader.Dispose();
        
        base.OnUnload();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        tex0.Use(TextureUnit.Texture0);
        tex1.Use(TextureUnit.Texture1);
        shader.Use();
        
        int id = shader.GetUniformLocation("color");
        GL.Uniform4(id, Color4.Aqua);
        
        GL.BindVertexArray(vertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        if (KeyboardState.IsKeyDown(Keys.Escape)) Close();
    }
}