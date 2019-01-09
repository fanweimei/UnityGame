# 知识点
1. 2D游戏，设置Camera的Projection为Orthographic，Default Behavior Mode为2D模式
2. 图片精灵：Sprite Mode设置为Multiple模式，Sprite Editor中点击Slice切割图片
3. 屏幕中物体的层级通过Sorting Layer和Order In Layer调节
4. SpringJoint2D组件：连接两个物体，做弹簧似的往返运动。
    - 给小鸟设置SpringJoint2D组件，给右边弹弓设置RigidBody2D组件，设置type为static，不受重力影响。
    - SpringJoint2D组件设置：设置Distance（距离）、Frequency(弹簧的强度)
5. 给小鸟添加`Box Colider`组件，可以点击；让小鸟的位置随鼠标移动
    - 先让屏幕坐标转化为世界坐标`Camera.main.ScreenToWordPoint(Input.mousePosition)`
6. 给右边的弹弓right添加一个rightPos的空子物体，用来设置位置；在小鸟和rightPos之间设置一个最大的距离。
7. 
